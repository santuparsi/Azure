@extends('layout')

@section('content')
<div class="jumbotron">
 <h1>Nate Client App</h1>

  @if(Auth::check())
    <h4 style="margin-top:1em;">Welcome {{ Auth::user()->name }}!</h4>
    <p>Let's start making calls to the Microsoft Graph API.</p>
  @else
    <p class="lead">Login to your Microsoft account in order to access the Microsoft Graph API</p>
    <a href="/signin" class="btn btn-primary btn-large">Click here to sign in</a>
  @endif
</div>
@endsection