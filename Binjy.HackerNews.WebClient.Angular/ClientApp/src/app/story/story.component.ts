import { Component, OnInit, Input } from '@angular/core';

import { Story } from '../story.service';

@Component({
  selector: 'app-story',
  templateUrl: './story.component.html'
})
export class StoryComponent implements OnInit {
    @Input() story: Story = {
        "title": "RaptorCS's redemption: the POWER9 machine works",
        "score": 50,
        "url": "https://drewdevault.com/2019/10/10/RaptorCS-redemption.html",
        "commentCount": 10,
        "author": "Sir_Cmpwn",
        "id": 21213897,
        "commentIndex": [
            21214434,
            21214456,
            21214866,
            21214116
        ],
        "time": "2019-10-10T13:01:40Z",
        "type": "story"
    };

    constructor() {}

    ngOnInit() {
        
    }
}


