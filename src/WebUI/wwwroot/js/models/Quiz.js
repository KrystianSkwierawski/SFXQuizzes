export var isAnswerCorrect = function (sfxId, userAnswer) {
    var fileName = window.answers.get(sfxId).split('.')[0].toLowerCase();
    var answers = fileName.split(' ` ');
    var isAnswerCorrect = answers.some(function (answer) { return answer === userAnswer; });
    return isAnswerCorrect;
};
//# sourceMappingURL=Quiz.js.map