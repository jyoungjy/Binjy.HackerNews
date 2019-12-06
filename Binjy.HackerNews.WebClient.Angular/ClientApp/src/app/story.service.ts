import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { STORIES } from './mock-stories';

@Injectable({
    providedIn: 'root',
})
export class StoryService {
    private baseUrl: string;

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    public getTopStories(limit: number = 10): Observable<Story[]> {
        return this.http.get<Story[]>(this.baseUrl + `api/hacker/stories/top/${limit}`);
    }

    public getNewestStories(limit: number = 10): Observable<Story[]> {
        return this.http.get<Story[]>(this.baseUrl + `api/hacker/stories/newest/${limit}`);
    }

    public getStoryById(id: number) {
        return this.http.get<Story>(this.baseUrl + `api/hacker/stories/${id}`);
    }
}

export interface NumberArray {
    [index: number]: number;
}

interface Item {
    author: string;
    id: number;
    commentIndex?: NumberArray | null;
    time: string;
    type: string;
}

export interface Story extends Item {
    title: string;
    score: number;
    url: string;
    commentCount: number;
}

export interface Comment extends Item {
    title: string;
    score: number;
    url: string;
    commentCount: number;
}

