import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { NgxGalleryOptions, NgxGalleryImage } from 'ngx-gallery';
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
    galleryImages: Observable<NgxGalleryImage[]>;
    galleryOptions: NgxGalleryOptions[] = [
        { 'previewCloseOnEsc': true, 'previewKeyboardNavigation': true, 'previewFullscreen': true, 'width': '800px',
            'height': '533px', 'imageSwipe': true },
        { 'breakpoint': 800, 'width': '600px', 'height': '400px' },
        { 'breakpoint': 500, 'width': '400px', 'height': '267px', 'thumbnailsColumns': 3 },
        { 'breakpoint': 300, 'width': '200px', 'height': '133px', 'thumbnailsColumns': 2 }
      ];

    constructor(private photoshootService: PhotoshootService, private route: ActivatedRoute) { }

    ngOnInit() {
        this.galleryImages = this.route.paramMap.switchMap((params: ParamMap) =>
            this.photoshootService.getImagesForPhotoshoot(params.get('name')).
            map((v) => v.map(image => ({ big : image.imageUri, medium : image.mediumUri, small: image.thumbnailUri }))));
    }
}
