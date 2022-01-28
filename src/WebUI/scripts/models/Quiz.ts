export const isAnswerCorrect = (sfxId, userAnswer) => {
    const fileName: string = (<any>window).answers.get(sfxId).split('.')[0].toLowerCase();
    const answers: string[] = fileName.split(' ` ');

    const isAnswerCorrect: boolean = answers.some(answer => answer === userAnswer);

    return isAnswerCorrect;
};