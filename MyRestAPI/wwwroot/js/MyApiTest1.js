'use strict';

class MyApiDemo
{
    constructor()
    {
    }

    SimpleAjax(ajaxObj, MyDataHandler, MyErrorHandler)
    {
        var request = $.ajax(ajaxObj);

        request.done(function (data)
        {
            MyDataHandler(data);
            MyErrorHandler( undefined );
        });

        
        request.fail(function (jqXHR, textStatus, err)
        {
            // just pass the jqXHR: Promise interface
            MyErrorHandler(jqXHR);
            MyDataHandler({});
        });
    }

    DisplayError(jqXHR)
    {

        var errView = $("#MyErrorText");

        if (jqXHR == undefined)
        {
            errView.text(' ');

            if (errView.hasClass("alert-danger")) {
                errView.removeClass("alert-danger");
            }
            errView.addClass("alert-success");
            errView.css("padding", "0px");
        }
        else
        {
            errView.text(jqXHR.status + ' Error : ' + jqXHR.statusText);

            if (errView.hasClass("alert-success"))
            {
                errView.removeClass("alert-success");
            }
            errView.css("padding", "10px");
            errView.addClass("alert-danger");
        }
    }


    DisplayJsonData(TheJsonData)
    {

        var jsonObj = undefined;
        if (typeof TheJsonData === 'string' || TheJsonData instanceof String)
        {
            jsonObj = JSON.parse(TheJsonData);
        }
        else
        {
            jsonObj = TheJsonData;
        }
        
        var jsonPretty = JSON.stringify(jsonObj, null, ' ');
        $('#MyJsonCodeArea').empty().append(jsonPretty);
        hljs.initHighlighting.called = false;
        hljs.initHighlighting();
    }

    TrySimpleGet()
    {
        var ajaxObj = {
            url: "api/MyProducts",
            method: "GET",
            dataType: "json"
        };

        ajaxObj.url = $('#MyUri1').val();

        this.SimpleAjax(ajaxObj, DemoObj.DisplayJsonData, DemoObj.DisplayError);
    }


    TryAuthorizedOnlyGet() {
        var ajaxObj = {
            url: "api/MyProducts/3",
            method: "GET",
            dataType: "json",
            headers: {
                'Accept':'application/json',
                'Content-Type':'application/json',
                'Authorization' : ' '
            }
        };

        ajaxObj.url = $('#MyUri2').val();
        var token = $("#AccessToken").text();
        ajaxObj.headers.Authorization = 'Bearer ' + token;

        this.SimpleAjax(ajaxObj, DemoObj.DisplayJsonData, DemoObj.DisplayError);
    }


    DisplayAccessToken(TheData)
    {
        console.log('AccessToken : ' + TheData.access_token);
        $("#AccessToken").text(TheData.access_token);
    }

    RequestAccessToken()
    {
        var ajaxObj = {
            url: "api/token",
            method: "POST",
            dataType: "json",
            headers: {
                'Accept':'application/json',
                'Content-Type':'application/x-www-form-urlencoded'
            },
            data: 'username=test123&password=test123',
        };

        this.SimpleAjax(ajaxObj, DemoObj.DisplayAccessToken, DemoObj.DisplayError);
    }


}

const DemoObj = new MyApiDemo();



// jQuery document ready
//$(document).ready(  function ()

$(() => {
    var LocalData = '{ "name": "Static Json Data","author": { "name": "Gandhi", "email": "123@gmail.com", "contact": [{"location": "office", "number": 123456}, {"location": "home", "number": 987654}] } }';

    //DisplayData(LocalData);
    DemoObj.DisplayJsonData(LocalData);
    DemoObj.DisplayError(undefined);
});