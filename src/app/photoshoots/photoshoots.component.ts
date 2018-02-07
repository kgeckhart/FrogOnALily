import { Component, OnInit } from '@angular/core';
import { PhotoshootService, IPhotoshoot, Photoshoot, PhotoshootCategory } from '../photoshootService';
import { Observable } from 'rxjs/Observable';

@Component({
    templateUrl: './photoshoots.component.html',
    styleUrls: ['./photoshoots.component.scss'],
    providers: [ PhotoshootService ]
})
export class PhotoshootsComponent implements OnInit {
    photoshoots: Observable<IPhotoshoot[]>;

    constructor(private photoshootService: PhotoshootService) { }

    ngOnInit() {
        this.photoshoots =  this.photoshootService.getPhotoshoots(undefined);
    }
}
