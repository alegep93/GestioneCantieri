angular.module("mainModule", [])
    .controller("listinoController", function ($scope, $http, $log) {
    $http.get('WebServices/ListinoService.asmx/GetListino')
        .then(function (response) {
            $scope.listini = response.data;
        });

    $scope.search = function (item) {
        if ($scope.searchText === undefined) {
            return true;
        } else {
            if (item.CodArt.toLowerCase().indexOf($scope.searchText.toLowerCase()) !== -1 ||
                item.Desc.toLowerCase().indexOf($scope.searchText.toLowerCase()) !== -1) {
                return true;
            }
        }

        return false;
    };
})