export const isAnswerCorrect = (sfxId, userAnswer) => {
    const fileName = window.answers.get(sfxId).split('.')[0].toLowerCase();
    const answers = fileName.split(' ` ');
    const isAnswerCorrect = answers.some(answer => answer === userAnswer);
    return isAnswerCorrect;
};
