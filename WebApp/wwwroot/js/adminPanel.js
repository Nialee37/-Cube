$(document).ready(function () {
    datatableVille();
    datatableCategorie();
    datatablePersonne();
    datatableRessource();
    datatableRessourceAll();
    requestChard();
    requestData();
    requestData2();
});

function datatableVille() {
    $('#manageVille').DataTable({
        "language": {
            "url": '../lib/datatables/fr-datatable.json'
        },
        "data": @ViewBag.listVille
        "ajax": {
            "url": "/Villes/GetAllVille",
            "dataSrc": ""
        },
        "columns": [
            { "data": "idVille" },
            { "data": "nom" },
            { "data": "cPostal" },


        ],
        "columnDefs": [

            {
                targets: 1,
                render: function (data, type, row, meta) {
                    return '<a target="_blank" href="/Villes/Edit/' + row.idVille + '">' + row.nom + '</a>'
                }

            }

        ]
    });
}
function datatableCategorie() {
    $('#manageCategorie').DataTable({
        "language": {
            "url": '../lib/datatables/fr-datatable.json'
        },
        "ajax": {
            "url": "/Categorie/GetAllCategorie",
            "dataSrc": ""
        },
        "columns": [
            { "data": "id" },
            { "data": "Libelle" },

        ],
        "columnDefs": [

            {
                targets: 1,
                render: function (data, type, row, meta) {
                    return '<a target="_blank" href="/Categorie/Edit/' + row.id + '">' + row.libelle + '</a>'
                }

            }

        ]

    });
}
function datatablePersonne() {
    $('#managePersonne').DataTable({
        "language": {
            "url": '../lib/datatables/fr-datatable.json'
        },
        "ajax": {
            "url": "/Personne/GetAllPersonne",
            "dataSrc": ""
        },
        "columns": [
            { "data": "id" },
            { "data": "nom" },
            { "data": "prenom" },
            { "data": "mail" },
            { "data": "IsActivate" }
        ],
        "columnDefs": [
            { visible: false, targets: [0] },
            {
                targets: 1,
                render: function (data, type, row, meta) {
                    return '<a target="_blank" href="/Personne/Edit/' + row.id + '">' + row.nom + '</a>'
                }

            },
            {
                targets: 4,
                render: function (data, type, row, meta) {
                    console.log(row);
                    return (row.isActivate ? '<span class="badge bg-success">Activer</span>' : '<span class="badge bg-danger">Désactiver</span>')
                }
            }
        ]

    });
}
function datatableRessource() {
    $('#manageRessource').DataTable({
        "language": {
            "url": '../lib/datatables/fr-datatable.json'
        },
        "ajax": {
            "url": "/Ressource/GetAllressourcetovalidate",
            "dataSrc": ""
        },
        "columns": [
            { "data": "id" },
            { "data": "nom" },
            { "data": "date" },
        ],
        "columnDefs": [
            { visible: false, targets: [0] },
            {
                targets: 1,
                render: function (data, type, row, meta) {
                    return '<a target="_blank" href="/Ressource/Edit/' + row.id + '">' + row.nom + '</a>'
                }
            }
        ]

    });
}
function datatableRessourceAll() {
    $('#manageRessourceAll').DataTable({
        "language": {
            "url": '../lib/datatables/fr-datatable.json'
        },
        "ajax": {
            "url": "/Ressource/GetAllressource",
            "dataSrc": ""
        },
        "columns": [
            { "data": "id" },
            { "data": "nom" },
            { "data": "date" },
            { "data": "idType" },
            { "data": "idCategorie" },


        ],
        "columnDefs": [
            { visible: false, targets: [0] },
            {
                targets: 1,
                render: function (data, type, row, meta) {
                    return '<a target="_blank" href="/Ressource/Edit/' + row.id + '">' + row.nom + '</a>'
                },
                //targets: 3,
                //render: function(data,type,row,meta){
                //    return '<p>' + row.idType + " " + row.idType '</p>'
                //},
                //targets: 4,
                //render: function(data,type,row,meta){
                //    return '<p>' + row.idCategorie + " " + row.idCategorie '</p>'
                //}
            }
        ]

    });
}
function requestChard() {
    chart2 = new Highcharts.chart('chart2', {
        chart: {
            type: 'line',
            events: {
                load: requestData2
            }
        },
        title: {
            text: 'Nombre de ressource par mois'
        },

        xAxis: {
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
        },
        yAxis: {
            title: {
                text: 'Nombre de ressource'
            }
        },
        plotOptions: {
            line: {
                dataLabels: {
                    enabled: true
                },
                enableMouseTracking: false
            }
        },
        series: [
            //    {
            //  name: 'nombre_de_ressources',
            //  data: [7, 6, 9, 14, 18, 21, 25, 26, 23, 18, 13, 9]
            //}
        ]
    });
}
function requestData() {
    var config1 = {
        chart: {
            type: 'column'

        },
        title: {
            text: 'Nombre de ressources par catégorie'
        },

        xAxis: {
            type: 'category',
            labels: {
                rotation: -45,
                style: {
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif'
                }
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Nombre de ressoruce'
            }
        },
        legend: {
            enabled: false
        },
        tooltip: {
            pointFormat: 'nombre de ressources'
        },
        series: []
    }


    $.ajax({
        url: '/Ressource/getressourcebycat/',
        type: "GET",

        success: function(response) {
            
            var data = new Array;
                var cat = new Array;
                for (var i = 0; i < response.length; i++) {
                    
                    cat[i] = response[i].name;
                    data[i] = {
                        y: response[i].value,
                        name: response[i].name,
                        color: response[i].color,
                        dataLabels: {
                            enabled: true,
                        }
                    }
                }
                config1.series = [{ data: data }];
                config1.xAxis.categories = cat

            
            var chart1 = new Highcharts.chart('chart1', config1 );
           
          
        },
        error :  function(response) {
            console.log(response);
        }
    });
}
function requestData2() {

    $.ajax({
        url: '/Ressource/getressourcebymonth/',
        type: "GET",

        success: function(response) {
            chart2.addSeries({
              name: "nombre_de_ressources",
              data: response
            });
        }
    });
}


    //  $.ajax({
    //    url: '/Ressource/getmoyressourcelu/',
    //    type: "GET",
    //    success: function(response) {
    //        console.log(response);
    //        document.getElementById("moyressourcelu").innerHTML = response;
    //    }
    //});

    // $.ajax({
    //    url: '/Ressource/moyressourcecree/',
    //    type: "GET",
    //    success: function(response) {
    //        console.log(response);
    //        document.getElementById("moyressourcecree").innerHTML = response;
    //    }
    //});