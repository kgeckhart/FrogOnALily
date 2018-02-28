import {InjectionToken} from '@angular/core';
import { API_BASE_URL } from './app/photoshootService';
import { environment } from './environments/environment';

export const API_BASE_URL_Env = { provide: API_BASE_URL, useValue: environment.API_BASE_URL };
