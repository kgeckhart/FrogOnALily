import { Component, OnInit } from '@angular/core';
import { NgxImageGalleryComponent, GALLERY_IMAGE, GALLERY_CONF } from 'ngx-image-gallery';
import { PhotoshootService, IPhotoshoot, Photoshoot, PhotoshootCategory } from '../photoshootService';
import { Observable } from 'rxjs/Observable';
import { IPhotoshootViewModel, PhotoshootViewModel } from './PhotoshootViewModel';

@Component({
    templateUrl: './photoshoots.component.html',
    styleUrls: ['./photoshoots.component.scss'],
    providers: [ PhotoshootService ]
})
export class PhotoshootsComponent implements OnInit {
    // photoshoots: Observable<IPhotoshootViewModel>;
    photoshoots: IPhotoshootViewModel[] = [];
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

    // ngOnInit() {
    //     this.photoshoots =  this.photoshootService.getPhotoshoots(undefined).flatMap((v) => v)
    //     .map(photoshoot => ({ id: photoshoot.id, url: photoshoot.thumbnailUri,
    //         category: photoshoot.category, shootDate: photoshoot.shootDate, name: photoshoot.name,
    //         images: [ { url : photoshoot.thumbnailUri }]}));
    // }
    ngOnInit() {
        this.photoshootService.getPhotoshoots(undefined).flatMap((v) => v)
            .map(photoshoot => ({ id: photoshoot.id, url: photoshoot.thumbnailUri,
                    category: photoshoot.category, shootDate: photoshoot.shootDate, name: photoshoot.name,
                    images: [ { url : photoshoot.thumbnailUri }]}))
            .subscribe(photoshoot => this.photoshoots.push(photoshoot));
    }
}
