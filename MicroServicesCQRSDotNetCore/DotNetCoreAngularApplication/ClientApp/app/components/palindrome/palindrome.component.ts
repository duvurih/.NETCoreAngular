import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Rx';

import { PalindromeService } from './palindrome.service';

import { PalindromeModel } from './../../common/models/palindrome.model';

@Component({
    selector: 'palindrome',
    templateUrl: './palindrome.component.html',
    styleUrls: ['./palindrome.component.css']
})
export class PalindromeComponent implements OnInit {

    palindromeItem: PalindromeModel = new PalindromeModel();

    palindromeData: PalindromeModel[] = [];

    dataSaved: boolean = false;

    constructor(private palindrome: PalindromeService) { }

    ngOnInit() {
        this.getPalindromes();
    }

    getPalindromes() {
        this.palindrome.getPalindromes().subscribe((data:any) => {
            this.palindromeData = data;
            console.log(data);
        })
    }

    VerifySavePalindrome() {
        console.log(this.palindromeItem);
        this.palindromeItem.id = 1;
        this.dataSaved = false;
        this.palindrome.savePalindrome(this.palindromeItem).subscribe((data: any) => {
            console.log(data);
            this.dataSaved = data;
            if (data == true) {
                this.getPalindromes();
            }
        });
    }
}
