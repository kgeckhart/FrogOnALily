import { Component, OnInit, Input } from '@angular/core';
import { NgxImageGalleryComponent, GALLERY_IMAGE, GALLERY_CONF } from 'ngx-image-gallery';
import { PhotoshootService, IPhotoshoot, Photoshoot } from '../photoshootService';
import { Observable } from 'rxjs/Observable';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    templateUrl: './photoshootgallery.component.html',
    styleUrls: ['./photoshootgallery.component.scss'],
    providers: [ PhotoshootService ]
})
export class PhotoshootGalleryComponent implements OnInit {
    @Input() photoshoot: IPhotoshoot;
    galleryImages: GALLERY_IMAGE[];
    galleryConfiguration: GALLERY_CONF = {
        showDeleteControl: false,
        showCloseControl: false,
        showExtUrlControl: false,
        closeOnEsc: true,
        showImageTitle: false,
        inline: false,
        reactToMouseWheel: false,
        backdropColor: 'default'
    };

    constructor(private photoshootService: PhotoshootService, private sanitizer: DomSanitizer) { }

    ngOnInit() {
        this.photoshootService.getImagesForPhotoshoot(this.photoshoot.id).
            flatMap((v) => v).map(image => ({ url : image.imageUri, thumbnailUrl: image.thumbnailUri })).
            subscribe((galleryImage) => (this.galleryImages.push(galleryImage)));
    }
}
