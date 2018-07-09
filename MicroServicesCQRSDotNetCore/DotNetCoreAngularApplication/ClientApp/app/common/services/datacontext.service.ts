import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers, ResponseContentType } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import "rxjs/add/operator/debounceTime";

@Injectable()
export class DataContextService {

    constructor(private http: Http) { }

    httpGet(url: string, parameters?: Object[]): Observable<any> {
        let observable: any;
        if (parameters != undefined) {
            const search = new URLSearchParams();
            for (let count = 0; count < parameters.length; count++) {
                search.set((parameters as any)[count][0], (parameters as any)[count][1]);
            }
            observable = this.http.get(url, { search }).map((response) => response.json());
        } else {
            observable = this.http.get(url).map((response) => response.json());
        }
        return observable;
    }

    httpPost(url: string, dataObject: any): Observable<any> {
        const _headers = new Headers();
        _headers.append("Content-Type", "application/json");
        _headers.append("Access-Control-Allow-Origin", location.origin);
        const observable = this.http.post(url, dataObject, { headers: _headers }).map(response => response.json());
        return observable;
    }

}
