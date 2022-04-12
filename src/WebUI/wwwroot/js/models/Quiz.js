export const isAnswerCorrect = (sfxId, userAnswer) => {
    const fileName = window.answers.get(sfxId).split('.')[0].toLowerCase();
    let breakingMark;
    if (fileName.includes(" ` "))
        breakingMark = " ` '";
    if (fileName.includes(" ' "))
        breakingMark = " ' ";
    if (fileName.includes(" ; "))
        breakingMark = " ; ";
    const answers = fileName.split(breakingMark);
    const isAnswerCorrect = answers.some(answer => answer === userAnswer);
    return isAnswerCorrect;
};
