import { getBreakingMark } from './Files.js';
export const isAnswerCorrect = (sfxId, userAnswer) => {
    const fileName = window.answers.get(sfxId).toLowerCase();
    const breakingMark = getBreakingMark(fileName);
    const answers = fileName.split(breakingMark);
    const isAnswerCorrect = answers.some(answer => answer === userAnswer);
    return isAnswerCorrect;
};
