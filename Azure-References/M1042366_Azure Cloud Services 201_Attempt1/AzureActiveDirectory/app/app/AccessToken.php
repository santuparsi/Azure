<?php

namespace App;

use Illuminate\Database\Eloquent\Model;
use League\OAuth2\Client\Provider\Exception\IdentityProviderException;

class AccessToken extends Model
{
    /**
     * The primary key associated with the table.
     *
     * @var string
     */
    protected $primaryKey = 'tokenId';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'user_id',
        'accessToken',
        'refreshToken',
        'tokenExpires',
    ];

    /**
     * Requests an access token
     * 
     * @param string $authCode
     * 
     * @return League\OAuth2\Client\Token\AccessTokenInterface
     */
    public static function requestAccessToken( $authCode ) {

        $oauthClient = app()['oauthClient'];

        try {
            // Make the token request
            $accessToken = $oauthClient->getAccessToken('authorization_code', [
              'code' => $authCode
            ]);

            return $accessToken;

        } catch (IdentityProviderException $e) {
            return null;
        }
    }
}
