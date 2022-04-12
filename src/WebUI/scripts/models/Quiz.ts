export const isAnswerCorrect = (sfxId, userAnswer) => {
    const fileName: string = (<any>window).answers.get(sfxId).split('.')[0].toLowerCase();

    let breakingMark: string;

    if (fileName.includes(" ` "))
        breakingMark = " ` '";

    if (fileName.includes(" ' "))
        breakingMark = " ' ";

    if (fileName.includes(" ; "))
        breakingMark = " ; ";

    const answers: string[] = fileName.split(breakingMark);

    const isAnswerCorrect: boolean = answers.some(answer => answer === userAnswer);

    return isAnswerCorrect;
};