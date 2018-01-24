import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { PhotoshootsComponent } from './photoshoots/photoshoots.component';
import { InvestmentComponent } from './investment/investment.component';

const appRoutes: Routes = [
  { path: 'welcome', component: HomeComponent},
  { path: 'about', component: AboutComponent},
  { path: 'investment', component: InvestmentComponent},
  { path: 'gallery', component: PhotoshootsComponent},
  { path: '', redirectTo: 'welcome', pathMatch: 'full'},
  { path: '**', redirectTo: 'welcome', pathMatch: 'full'}
];

@NgModule({
  imports: [ RouterModule.forRoot(appRoutes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
