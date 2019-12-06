import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { StoryComponent } from './story/story.component';
import { StoryListComponent } from './story-list/story-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    StoryComponent,
    StoryListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: StoryListComponent, data: { qualifier: 'top' } },
        { path: 'topstories', component: StoryListComponent, data: { qualifier: 'top' } },
        { path: 'neweststories', component: StoryListComponent, data: { qualifier: 'newest' } },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
