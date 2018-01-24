import { PhotoshootCategory } from '../photoshootService';
import { GALLERY_IMAGE } from 'ngx-image-gallery';

export interface IPhotoshootViewModel {
    id: number;
    name?: string;
    category: PhotoshootCategory;
    images?: GALLERY_IMAGE[];
    shootDate: Date;
}

export class PhotoshootViewModel {
    id: number;
    name?: string;
    category: PhotoshootCategory;
    images?: GALLERY_IMAGE[];
    shootDate: Date;
}
