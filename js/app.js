let editor;

window.initCKEditor = function (dotNetRef) {
    editor = CKEDITOR.replace(document.querySelector('#editor'));
    editor.on('change', function() {
        dotNetRef.invokeMethodAsync('OnEditorChange');
    });
}

window.getCKEditorContent = function () {
    return editor.getData();
}