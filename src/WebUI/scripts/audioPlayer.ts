import { elements } from './views/base.js';
import { getVolumeInputValue } from './views/quizCreatorView.js';

let _audio: HTMLAudioElement;

elements.audioPlayer__startButtons.forEach(button => {
    button.addEventListener('click', (e: any) => {
        const id: string = button.id;

        if (_audio)
            _audio.pause();

        const url: Location = window.location;

        if (url.search)
            _audio = new Audio(`./assets/audios/${url.search}/${id}.wav`);

        _audio = new Audio(`./assets/audios/demo/${id}.wav`);

        const volume: number = +getVolumeInputValue() / 100;
        _audio.volume = volume;
        _audio.play();
    })
});

