import { getBreakingMark } from './Files.js';

export const isAnswerCorrect = (sfxId, userAnswer) => {
    const fileName: string = (<any>window).answers.get(sfxId).toLowerCase();

    const breakingMark = getBreakingMark(fileName);

    const answers: string[] = fileName.split(breakingMark);

    const isAnswerCorrect: boolean = answers.some(answer => answer === userAnswer);

    return isAnswerCorrect;
};