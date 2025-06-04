<?php

namespace App;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Foundation\Auth\User as Authenticatable;

class User extends Authenticatable
{
    protected $primaryKey = 'id';

    protected $fillable = [
        'name',
        'email',
        'remember_token',
    ];
}
