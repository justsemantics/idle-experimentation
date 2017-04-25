// JavaScript source code

$(document).ready(
    function () {
        $("input").on("input", function () { generateLogoSVG() });
        generateLogoSVG();
    });

function generateLogoSVG() {
    var radius = Number($("#radius").val());
    var width = Number($("#width").val());
    var overlap = Number($("#overlap").val());
    var spacing = Number($("#spacing").val());

    var centerX = 1920 / 2;
    var centerY = 1080 / 2;

    var newHTML = "<svg width =\"1920\" height=\"1080\">";

    var offsetX = spacing;
    var startX = centerX - offsetX * 1.5;

    for (var i = 0; i < 4; i++) {
        newHTML += generateCircleXML(startX + i * offsetX, centerY);
    }

    //close svg
    newHTML += "</svg>";

    $("#SVGArea").html(newHTML);


    function generateCircleXML(x, y) {
        var generatedHTML = "";

        //center circle
        generatedHTML += "<circle cx=\"" + x + "\" cy=\"" + y + "\" r=\"" + width + "\" stroke=\"black\" stroke-width=\"2\" fill=\"none\" />";

        //larger surrounding center circles
        generatedHTML += "<circle cx=\"" + x + "\" cy=\"" + y + "\" r=\"" + overlap + "\" stroke=\"black\" stroke-width=\"2\" fill=\"none\" />";
        generatedHTML += "<circle cx=\"" + x + "\" cy=\"" + y + "\" r=\"" + (overlap + width) + "\" stroke=\"black\" stroke-width=\"2\" fill=\"none\" />";

        //left large circles
        generatedHTML += "<circle cx=\"" + (x - radius + overlap) + "\" cy=\"" + y + "\" r=\"" + radius + "\" stroke=\"black\" stroke-width=\"2\" fill=\"none\" />";
        generatedHTML += "<circle cx=\"" + (x - radius + overlap) + "\" cy=\"" + y + "\" r=\"" + (radius + width) + "\" stroke=\"black\" stroke-width=\"2\" fill=\"none\" />";

        //right large circles
        generatedHTML += "<circle cx=\"" + (x + radius - overlap) + "\" cy=\"" + y + "\" r=\"" + radius + "\" stroke=\"black\" stroke-width=\"2\" fill=\"none\" />";
        generatedHTML += "<circle cx=\"" + (x + radius - overlap) + "\" cy=\"" + y + "\" r=\"" + (radius + width) + "\" stroke=\"black\" stroke-width=\"2\" fill=\"none\" />";

        return generatedHTML;
    }
}