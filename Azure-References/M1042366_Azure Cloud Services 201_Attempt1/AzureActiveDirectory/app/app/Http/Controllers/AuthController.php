<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use League\OAuth2\Client\Provider\GenericProvider;
use League\OAuth2\Client\Provider\Exception\IdentityProviderException;
use Microsoft\Graph\Graph;
use Microsoft\Graph\Model;
use Illuminate\Support\Facades\Auth;
use App\AccessToken;
use App\User;

class AuthController extends Controller
{

  /**
   * @var GenericProvider
   */
  protected $oauthClient;

  /**
   * Constructor for the AuthController
   */
  public function __construct() {

    // Create the OAuth client
    $this->oauthClient = app()['oauthClient'];
  }

  public function signin() {
    $authUrl = $this->oauthClient->getAuthorizationUrl();

    // Save the client state so we can validate it in the callback
    session(['oauthState' => $this->oauthClient->getState()]);

    // Redirect to AAD signin page
    return redirect()->away( $authUrl );
  }

  /**
   * The callback function for the OAuth sign in
   */
  public function oAuthCallback(Request $request) {
    // Validate state
    $expectedState = session('oauthState');
    $request->session()->forget('oauthState');
    $providedState = $request->query('state');

    if (!isset($expectedState)) {
      // If there is no expected state in the session,
      // redirect to the home page with an error.
      return redirect('/')
        ->with('error', 'No session state')
        ->with('errorDetail', 'Please try signing in again.');
    }

    if(!isset($providedState) || $expectedState != $providedState) {
      return redirect('/')
        ->with('error', 'Invalid authorization state')
        ->with('errorDetail', 'The authorization state did not match');
    }

    // Get the authorization code from the request.
    $authCode = $request->query('code');

    if (isset($authCode)) {

      $accessToken = AccessToken::requestAccessToken( $authCode );

      if( ! $accessToken ) {
        return redirect('/')
          ->with('error', 'Error while trying to retrieve access token')
          ->with('errorDetail', $e->getMessage());
      }
    
      // Get the user's details
      $graph = new Graph();
      $graph->setAccessToken($accessToken->getToken());
    
      $msUser = $graph->createRequest('GET', '/me')
        ->setReturnType(Model\User::class)
        ->execute();

      $userEmail = !empty( $msUser->getMail() ) ? $msUser->getMail() : $msUser->getUserPrincipalName();

      // Get or create the user
      $user = User::firstOrCreate(
        ['email' => $userEmail],
        ['name' => $msUser->getDisplayName()]
      );

      // Store the access token and user details
      AccessToken::firstOrCreate(
        [
          'user_id' => $user->id
        ],
        [
          'accessToken' => $accessToken->getToken(),
          'refreshToken' => $accessToken->getRefreshToken(),
          'tokenExpires' => $accessToken->getExpires(),
        ]
      );

      // Login the user
      Auth::loginUsingId($user->id, true);
      
      return redirect('/')
        ->with('status', 'Access token created.');

    }

    return redirect('/')
      ->with('error', $request->query('error'))
      ->with('errorDetail', $request->query('error_description'));
  }


  /**
   * Sign out
   */
  public function signout() {

    if(!Auth::check()) {
      return redirect('/')->with('error', 'You are not logged in.');
    }

    Auth::logout();

    return redirect('/')->with('status', "You have successfully logged out.");
  }
}