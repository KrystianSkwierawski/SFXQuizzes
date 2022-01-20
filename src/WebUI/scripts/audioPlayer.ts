import { elements } from './views/base.js';
import * as quizCreatorView from './views/quizCreatorView.js';

let _audio: HTMLAudioElement;
let _linkedVolumes: boolean;

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
        if (!_linkedVolumes)
            return;

        const volume: string = (<HTMLInputElement>input).value;
        setLinkedVolumeInputs(volume);
    });
});

function setLinkedVolumeInputs(volume: string) {
    elements.volumeInputs.forEach(input => {
        (<HTMLInputElement>input).value = volume;
    })
}



