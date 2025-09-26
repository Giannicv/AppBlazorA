
    function descargarArchivo(base64Data, fileName)
    {
        var link= document.createElement("a");
    link.href="data:application/pdf;base64,"+base64Data;
    link.download = fileName;

    link.click();
    }
