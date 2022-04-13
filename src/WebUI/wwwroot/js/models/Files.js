export const validateFiles = (files) => {
    const numberOfFilesError = validateNumberOfFiles(files);
    if (numberOfFilesError)
        return numberOfFilesError;
    const filesSizeError = validateFilesSize(files);
    if (filesSizeError)
        return filesSizeError;
    const supportedFormatsError = validateSupportedFormats(files);
    if (supportedFormatsError)
        return supportedFormatsError;
    return;
};
const validateNumberOfFiles = (files) => {
    if (files.length > 30)
        return 'Number of files must less than 30';
    return;
};
const validateFilesSize = (files) => {
    const tooLargeFiles = Array.from(files).filter(file => file.size > 300 * 1024);
    if (tooLargeFiles.length > 0) {
        let validationError = 'File size must under 300KB\n';
        tooLargeFiles.forEach(file => {
            validationError += `\n${file.name}: ${Math.floor(file.size / 1024)}KB`;
        });
        return validationError;
    }
    return;
};
const validateSupportedFormats = (files) => {
    const supportedFormats = ['mp3', 'wav', 'ogg'];
    const notSupportedFiles = Array.from(files).filter(file => !supportedFormats.includes(file.name.split('.')[1].toLowerCase()));
    if (notSupportedFiles.length > 0) {
        let validationError = 'File must be supported audio format - mp3, wav, ogg\n';
        notSupportedFiles.forEach(file => {
            validationError += `\n${file.name}`;
        });
        return validationError;
    }
    return;
};
export const getBreakingMark = (name) => {
    let breakingMark;
    if (name.includes(" ` "))
        breakingMark = " ` ";
    if (name.includes(" ' "))
        breakingMark = " ' ";
    if (name.includes(" ; "))
        breakingMark = " ; ";
    return breakingMark;
};
