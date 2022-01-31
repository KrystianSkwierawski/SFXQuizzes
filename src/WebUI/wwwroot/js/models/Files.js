export var validateFiles = function (files) {
    var numberOfFilesError = validateNumberOfFiles(files);
    if (numberOfFilesError)
        return numberOfFilesError;
    var filesSizeError = validateFilesSize(files);
    if (filesSizeError)
        return filesSizeError;
    var supportedFormatsError = validateSupportedFormats(files);
    if (supportedFormatsError)
        return supportedFormatsError;
    return;
};
var validateNumberOfFiles = function (files) {
    if (files.length > 30)
        return 'Number of files must less than 30';
    return;
};
var validateFilesSize = function (files) {
    var tooLargeFiles = Array.from(files).filter(function (file) { return file.size > 300 * 1024; });
    if (tooLargeFiles.length > 0) {
        var validationError_1 = 'File size must under 300KB\n';
        tooLargeFiles.forEach(function (file) {
            validationError_1 += "\n".concat(file.name, ": ").concat(Math.floor(file.size / 1024), "KB");
        });
        return validationError_1;
    }
    return;
};
var validateSupportedFormats = function (files) {
    var supportedFormats = ['mp3', 'wav', 'ogg'];
    var notSupportedFiles = Array.from(files).filter(function (file) { return !supportedFormats.includes(file.name.split('.')[1].toLowerCase()); });
    if (notSupportedFiles.length > 0) {
        var validationError_2 = 'File must be supported audio format - mp3, wav, ogg\n';
        notSupportedFiles.forEach(function (file) {
            validationError_2 += "\n".concat(file.name);
        });
        return validationError_2;
    }
    return;
};
//# sourceMappingURL=Files.js.map