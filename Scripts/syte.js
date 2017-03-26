
function changeColor(id) {

    var ul = $('#ulProjectList');
    var list = ul.children();
    $.each(list, function (i, item) {
        //item.css("background-color", "rgb(49, 109, 109)");
        item.style.backgroundColor = "rgb(49, 109, 109)";
    });
    $('#' + id).css("background-color", "rgb(56, 239, 239)");
}

    function Hide() {
        $(this).parent().remove();
    }

    function writeHtml(item) {
                    var html = '<div class="col-md-3">' +
                                  '<div class="panel panel-warning" style="height:28vh">' +
                                    '<div class="panel-heading">' +
                                        '<h3 class="panel-title">' +
                                     '<a href="/Projects/PublicProjectData/' + item.ProjectId + '">' +
                                        item.Name + '</a></h3>' +
                                    '</div>' +
                                    '<div class="panel-body" style="overflow:hidden;overflow-wrap:break-word;height:70%">';
                                       if (item.Description != null) {
                                           html = html + item.Description.substring(0, 120)
                                            if ((item.Description).length > 120) {
                                                html = html + "...";
                                            }
                                        }
                                        else {
                                            html = html + "Projeto sem descrição."
                                        }
        html = html +
                                    '</div>' +
                                 '</div>' +
                              '</div>';

        return html;
        //$('#publicProjectsPage').append(html);
                    }

    function loadpublicHome() {
        $.ajax({
            url: 'Projects/LoadPublicProjects',
            type: 'get',
            data: { index: 0 },
            error: function (jqxhr, status, error) {
                var x = 10;
            },
            success: function (data, status, jqxhr) {
                $('#publicProjects').html('');
                $.each(data, function (i, item) {
                    var html = writeHtml(item);
                    
                    $('#publicProjects').append(html);
                    if (i == 3) {
                        return false;
                    }

                });
            }
        });
    }


    function loadpublicPage() {
        if (!publicProjectCounter) {
            publicProjectCounter = 0;
        }
        $.ajax({
            url:  'LoadPublicProjects',
            type: 'get',
            data: { index: publicProjectCounter},
            error: function (jqxhr, status, error) {
                var x = 10;
            },
            success: function (data, status, jqxhr) {
                $.each(data, function (i, item) {
                    publicProjectCounter = publicProjectCounter+1;
                        var html = writeHtml(item);

                        $('#publicProjectsPage').append(html);
                    

                });
            }
        });
    }

    function loadSearchList() {
        if (typeof publicProjectCounter == 'undefined') {
            publicProjectCounter = 0;
        }
        var searchkey = document.getElementById('searchKey').value;
        var e = document.getElementById('selectSearch');
        var cam = e.options[e.selectedIndex].id;
        var el = document.getElementById('selectOrderBy');
        var orderby = e.options[el.selectedIndex].id;
        $.ajax({
            data: { searchKey: searchkey, campo: cam, orderBy: orderby, index: publicProjectCounter },
            url: 'LoadPublicProjects',
            type: 'GET',
            error: function (jqXHR, status, error) {
                var x = 10;
            },
            success: function (data, status, jqXHR) {
                if (data.length == 0) {
                    alert("Não há mais projetos para carregar");
                }
                $.each(data, function (i, item) {
                    publicProjectCounter = publicProjectCounter + 1;
                   var html =  writeHtml(item);
                    $('#publicProjectsPage').append(html);
                });
            }

            
        });
    }


        function SendStatus(text) {
            $('#statusMessage').text(text);
        }
        function AtualizaLista(id) {
            var novoNome = $("#nomeCampo").val();
            $("#item" + id).text(novoNome);
    }

              
    function testando(text, id) {
        SendStatus(text);
        AtualizaLista(id);
    }


 