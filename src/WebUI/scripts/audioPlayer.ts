import { elements, elementStrings } from './views/base.js';
import * as quizCreatorView from './views/quizCreatorView.js';

let _audio: HTMLAudioElement;
let _linkedVolume: boolean;

elements.audioPlayer__startButtons.forEach(button => {
    button.addEventListener('click', (e: any) => {
        const id: string = button.id;

        if (_audio)
            _audio.pause();

        const url: Location = window.location;

        if (url.search)
            _audio = new Audio(`./assets/audios/${url.search}/${id}.wav`);

        _audio = new Audio(`./assets/audios/demo/${id}.wav`);

        const volume: number = +quizCreatorView.getVolumeInputValue(button.parentElement) / 100;
        _audio.volume = volume;
        _audio.play();
    })
});

elements.linkVolumeButtons.forEach(button => {
    button.addEventListener('click', () => {
        quizCreatorView.showUnlinkVolumeButton();
        _linkedVolume = false;
    });
});

elements.unlinkVolumeButtons.forEach(button => {
    button.addEventListener('click', () => {
        quizCreatorView.showLinkVolumeButton();
        _linkedVolume = true;
    });
});

elements.volumeInputs.forEach(input => {
    input.addEventListener('input', () => {
        if (!_linkedVolume)
            return;

        const volume: string = (<HTMLInputElement>input).value;

        elements.volumeInputs.forEach(input => {     
            (<HTMLInputElement>input).value = volume;
        })
    });
});



