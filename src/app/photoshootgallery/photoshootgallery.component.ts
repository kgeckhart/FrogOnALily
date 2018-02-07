import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { NgxImageGalleryComponent, GALLERY_IMAGE, GALLERY_CONF } from 'ngx-image-gallery';
import { PhotoshootService, IPhotoshoot, Photoshoot } from '../photoshootService';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';

@Component({
    templateUrl: './photoshootgallery.component.html',
    styleUrls: ['./photoshootgallery.component.scss'],
    providers: [ PhotoshootService ]
})
export class PhotoshootGalleryComponent implements OnInit {
    galleryImages: Observable<GALLERY_IMAGE[]>;
    galleryConfiguration: GALLERY_CONF = {
        showExtUrlControl: false,
        showImageTitle: false,
        showThumbnails : false,
        showCloseControl: false,
        inline: true,
        reactToMouseWheel: false,
        backdropColor: 'default'
    };

    constructor(private photoshootService: PhotoshootService, private route: ActivatedRoute) { }

    ngOnInit() {
        this.galleryImages = this.route.paramMap.switchMap((params: ParamMap) =>
            this.photoshootService.getImagesForPhotoshoot(params.get('name')).
            map((v) => v.map(image => ({ url : image.imageUri, thumbnailUrl: image.thumbnailUri }))));
    }
}
