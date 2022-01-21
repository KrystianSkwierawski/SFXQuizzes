import { elements } from './views/base.js';
import * as quizCreatorView from './views/quizCreatorView.js';

let _audio: HTMLAudioElement = new Audio();
let _linkedVolumes: boolean;

elements.audioPlayer__startButtons.forEach(button => {
    button.addEventListener('click', () => {
        const id: string = (<HTMLElement>button.parentNode).id;

        if (!_audio.ended)
            _audio.pause();

        const url: Location = window.location;

        if (url.search)
            _audio.src = `./assets/audios/${url.search}/${id}.wav`;

        _audio.src = `./assets/audios/demo/${id}.wav`;

        const volume: number = +quizCreatorView.getVolumeInputValue(button.parentElement) / 100;
        _audio.volume = volume;

        _audio.play();
    })
});

elements.linkVolumeButtons.forEach(button => {
    button.addEventListener('click', () => {
        quizCreatorView.showUnlinkVolumeButton();
        _linkedVolumes = false;
    });
});

elements.unlinkVolumeButtons.forEach(button => {
    button.addEventListener('click', () => {
        quizCreatorView.showLinkVolumeButton();
        _linkedVolumes = true;
    });
});

elements.volumeInputs.forEach(input => {
    input.addEventListener('input', () => {
        const volumeInputValue: string = (<HTMLInputElement>input).value;

        _audio.volume = +volumeInputValue / 100;

        if (!_linkedVolumes)
            return;

        setLinkedVolumeInputs(volumeInputValue);
    });
});

function setLinkedVolumeInputs(volume: string) {
    elements.volumeInputs.forEach(input => {
        (<HTMLInputElement>input).value = volume;
    })
}

elements.sfxNameInputs.forEach((input: HTMLElement) => {
    input.addEventListener('keyup', (e: any) => {
        if (e.key === 'Enter' || e.keyCode === 13) {
            const id: string = (<HTMLElement>input.parentNode).id;

            const userAnswer: string = (<string>e.target.value).toLowerCase();
            const answer: string = (<any>window).answers.get(id).toLowerCase();

            const isAnswerCorrect: boolean = answer === userAnswer;

            if (isAnswerCorrect) {
                quizCreatorView.setInputToAnsweredCorrectly(input);
                quizCreatorView.addOnePointToCurrentScore();

                return;
            }

            quizCreatorView.setInputToAnsweredInCorrectly(input);
        }
    });
});




