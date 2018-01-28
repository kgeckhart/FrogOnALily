import { Component, OnInit } from '@angular/core';
import { NgxImageGalleryComponent, GALLERY_IMAGE, GALLERY_CONF } from 'ngx-image-gallery';
import { PhotoshootService, IPhotoshoot, Photoshoot, PhotoshootCategory } from '../photoshootService';
import { Observable } from 'rxjs/Observable';

@Component({
    templateUrl: './photoshoots.component.html',
    styleUrls: ['./photoshoots.component.scss'],
    providers: [ PhotoshootService ]
})
export class PhotoshootsComponent implements OnInit {
    photoshoots: Observable<IPhotoshoot[]>;
    galleryConfiguration: GALLERY_CONF = {
        showDeleteControl: false,
        showCloseControl: false,
        showExtUrlControl: false,
        closeOnEsc: true,
        showImageTitle: false,
        inline: true,
        reactToMouseWheel: false,
        showThumbnails: false,
        backdropColor: 'default'
    };

    constructor(private photoshootService: PhotoshootService) { }

    ngOnInit() {
        this.photoshoots =  this.photoshootService.getPhotoshoots(undefined);
    }
}
