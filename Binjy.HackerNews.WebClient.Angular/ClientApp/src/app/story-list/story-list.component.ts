import { Component, Inject, OnInit } from '@angular/core';
import { Story, StoryService, NumberArray } from '../story.service';
import { StoryComponent } from '../story/story.component';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-story-list',
  templateUrl: './story-list.component.html'
})
export class StoryListComponent implements OnInit {
    stories: Story[] = [];
    limits: NumberArray = [10, 50, 100, 200];
    selectedLimit: number = 10;
    listTitle: string;
    qualifier: string;

    constructor(private storyService: StoryService, private route: ActivatedRoute) {}

    ngOnInit() {
        //getting context data from route
        this.route.data.subscribe(data => {
            this.qualifier = data["qualifier"];
        })

        this.update();
    }

    update() {
        //getting top stories or newest stories based on context
        if (this.qualifier == "top") {
            this.showTopStories();
        }
        else {
            this.showNewestStories();
        }
    }

    // get the top stories
    showTopStories() {
        this.listTitle = "Top Stories";
        this.storyService.getTopStories(this.selectedLimit).subscribe(result => {
            this.stories = result;
        }, error => console.error(error));
    }

    // get the newest stories
    showNewestStories() {
        this.listTitle = "Newest Stories";
        this.storyService.getNewestStories(this.selectedLimit).subscribe(result => {
            this.stories = result;
        }, error => console.error(error));
    }

    //requery when items per page is changed
    onLimitChanged(limitVal: number) {
        this.selectedLimit = limitVal;

        this.update();
    }
}


