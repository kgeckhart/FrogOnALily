import { Component, OnInit, Input, Inject} from '@angular/core';
import { NgxImageGalleryComponent, GALLERY_IMAGE, GALLERY_CONF } from 'ngx-image-gallery';
import { MatDialog, MAT_DIALOG_DATA} from '@angular/material';
import { PhotoshootService, IPhotoshoot, Photoshoot } from '../photoshootService';
import { Observable } from 'rxjs/Observable';

@Component({
    templateUrl: './photoshootgallery.component.html',
    styleUrls: ['./photoshootgallery.component.scss'],
    providers: [ PhotoshootService ]
})
export class PhotoshootGalleryComponent implements OnInit {
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

    constructor(@Inject(MAT_DIALOG_DATA) private photoshoot: IPhotoshoot, private photoshootService: PhotoshootService) { }

    ngOnInit() {
        this.photoshootService.getImagesForPhotoshoot(this.photoshoot.name).
            flatMap((v) => v).map(image => ({ url : image.imageUri, thumbnailUrl: image.thumbnailUri })).
            subscribe((galleryImage) => (this.galleryImages.push(galleryImage)));
    }
}
