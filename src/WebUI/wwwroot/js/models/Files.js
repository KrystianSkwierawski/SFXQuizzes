const supportedFormats = ['mp3', 'wav', 'ogg'];
export const validateFiles = (files) => {
    let validationError;
    if (files.length > 30)
        validationError = 'Number of files must less than 30';
    [...files].forEach(file => {
        if (file.size > 307200)
            validationError = `File size must under 300KB\n\n${file.name}: ${Math.floor(file.size / 1024)}KB \n`;
        const format = file.name.split('.')[1];
        if (!supportedFormats.some(supportedFormat => supportedFormat === format.toLocaleLowerCase()))
            validationError = `File must be supported audio format - mp3, wav, ogg\n\n${file.name}`;
    });
    return validationError;
};
//# sourceMappingURL=Files.js.map