function hasImageBeenSaved()
{
    var n = app.activeDocument.name;
    try
    {
        var myPath = app.activeDocument.path;
        return true;
    }
    catch(e)
    {
        return false;
    }
}

function setSelection(doc, sidePos) {
    app.activeDocument = doc;
    var x1 = sidePos[0]*size, y1 = sidePos[1]*size;
    var x2 = (sidePos[0]+1)*size, y2 = (sidePos[1]+1)*size;
    var selection = Array(Array(x1, y1), Array(x2, y1), Array(x2, y2), Array(x1, y2));
    doc.selection.select(selection);
}

function copyCubeFace(blenderPos, unityPos) {
    setSelection(blenderDoc, blenderPos);
    blenderDoc.selection.copy();

    setSelection(unityDoc, unityPos);
    unityDoc.paste(true);
}

var blenderDoc, unityDoc;
var size;

function main() {    
    if(!hasImageBeenSaved())
    {
        alert("Source cubemap cannot be a new file, it must have been saved and have a file path");
        return;
    }

    var originalRulerUnits = app.preferences.rulerUnits;
    var originalTypeUnits = app.preferences.typeUnits;

    app.preferences.rulerUnits = Units.PIXELS;
    app.preferences.typeUnits = TypeUnits.PIXELS;

    blenderDoc = app.activeDocument;
    size = blenderDoc.width / 3;

    unityDoc = app.documents.add(size*6, size, 72, "UnityCubemap")

    copyCubeFace(Array(0, 0), Array(0, 0));
    copyCubeFace(Array(2, 0), Array(1, 0));
    copyCubeFace(Array(1, 1), Array(2, 0));
    copyCubeFace(Array(0, 1), Array(3, 0));
    copyCubeFace(Array(1, 0), Array(4, 0));
    copyCubeFace(Array(2, 1), Array(5, 0));

    unityDoc.saveAs(new File(blenderDoc.path + "/Cubemap.png"), new PNGSaveOptions(), true, Extension.LOWERCASE);
    unityDoc.close(SaveOptions.DONOTSAVECHANGES);

    blenderDoc.selection.deselect();

    app.preferences.rulerUnits = originalRulerUnits;
    app.preferences.typeUnits = originalTypeUnits;
}


main();