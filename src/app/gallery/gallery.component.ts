import { Component, OnInit, Input } from '@angular/core';
import { NgxImageGalleryComponent, GALLERY_IMAGE, GALLERY_CONF } from 'ngx-image-gallery';
import { PhotoshootService, IPhotoshoot, Photoshoot } from '../photoshootService';
import { Observable } from 'rxjs/Observable';

@Component({
    templateUrl: './gallery.component.html',
    styleUrls: ['./gallery.component.scss'],
    providers: [ PhotoshootService ]
})
export class GalleryComponent implements OnInit {
    @Input() photoshoot: IPhotoshoot;
    galleryImages: GALLERY_IMAGE[];
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
        let images = this.photoshootService.getImagesForPhotoshoot(this.photoshoot.id).
            map(photoshootImages => photoshootImages.map(photoshootImage =>
                ({ url : photoshootImage.imageUri, thumbnailUrl: photoshootImage.thumbnailUri })));
    }
}
