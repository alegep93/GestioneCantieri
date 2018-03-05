/// <reference path="../scripts/angular.min.js" />

var app = angular.module("mainModule", [])
    .config(['$qProvider', function ($qProvider) {
        $qProvider.errorOnUnhandledRejections(false);
    }]);
