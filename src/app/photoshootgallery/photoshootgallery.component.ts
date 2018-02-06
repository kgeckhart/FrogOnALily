import { Component, OnInit, Inject} from '@angular/core';
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
    galleryImages: GALLERY_IMAGE[] = new Array();
    galleryConfiguration: GALLERY_CONF = {
        showDeleteControl: false,
        showCloseControl: true,
        showExtUrlControl: false,
        closeOnEsc: true,
        showImageTitle: false,
        inline: false,
        reactToMouseWheel: false,
        backdropColor: 'default'
    };

    constructor(@Inject(MAT_DIALOG_DATA) private photoshootName: string, private photoshootService: PhotoshootService) { }

    ngOnInit() {
        console.log('init called ' + this.photoshootName);
        this.photoshootService.getImagesForPhotoshoot(this.photoshootName).
            flatMap((v) => v).map(image => ({ url : image.imageUri, thumbnailUrl: image.thumbnailUri })).
            subscribe((galleryImage) => (this.galleryImages.push(galleryImage)));
    }
}
