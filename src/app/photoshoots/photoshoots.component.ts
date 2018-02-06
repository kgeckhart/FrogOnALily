import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { PhotoshootService, IPhotoshoot, Photoshoot, PhotoshootCategory } from '../photoshootService';
import { Observable } from 'rxjs/Observable';
import { PhotoshootGalleryComponent } from '../photoshootgallery/photoshootgallery.component';

@Component({
    templateUrl: './photoshoots.component.html',
    styleUrls: ['./photoshoots.component.scss'],
    providers: [ PhotoshootService ]
})
export class PhotoshootsComponent implements OnInit {
    photoshoots: Observable<IPhotoshoot[]>;

    constructor(private photoshootService: PhotoshootService, public dialog: MatDialog) { }

    ngOnInit() {
        this.photoshoots =  this.photoshootService.getPhotoshoots(undefined);
    }

    openDialog(photoshoot: IPhotoshoot): void {
        console.log('dialog should open');
        this.dialog.open(PhotoshootGalleryComponent, {
            data: photoshoot
        });
    }
}
