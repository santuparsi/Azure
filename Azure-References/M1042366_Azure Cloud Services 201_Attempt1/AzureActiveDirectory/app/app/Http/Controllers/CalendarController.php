<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\AccessToken;
use Illuminate\Support\Facades\Auth;
use Microsoft\Graph\Graph;
use Microsoft\Graph\Model;

class CalendarController extends Controller
{
    /**
     * Access the calendar API
     */
    public function calendar(Request $request) {
      // Get the access token from the cache
      $accessToken = AccessToken::where('user_id', Auth::user()->id)->first();
  
      // Create a Graph client
      $graph = new Graph();
      $graph->setAccessToken($accessToken->accessToken);
  
      $queryParams = [
        '$select' => 'subject,organizer,start,end',
        '$orderby' => 'createdDateTime DESC'
      ];
  
      // Append query parameters to the '/me/events' url
      $getEventsUrl = '/me/events?' . http_build_query($queryParams);
  
      $events = $graph->createRequest('GET', $getEventsUrl)
        ->setReturnType(Model\Event::class)
        ->execute();
  
      return view('calendar', ['events' => $events]);
    }
}
