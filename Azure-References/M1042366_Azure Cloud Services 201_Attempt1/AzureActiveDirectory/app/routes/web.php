<?php

use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', 'HomeController@welcome');

Route::get('/signin', 'AuthController@signin');
Route::get('/oauth/callback', 'AuthController@oAuthCallback');
Route::get('/signout', 'AuthController@signout');

Route::get('/calendar', 'CalendarController@calendar');