import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-comment-list',
    templateUrl: './comment-list.component.html'
  })
  export class CommentListComponent implements OnInit {
    comments: Comment[] = [];
    ngOnInit(): void {
      throw new Error('Method not implemented.');
    }
}
