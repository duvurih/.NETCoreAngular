import { Injectable, Inject } from '@angular/core';

import { DataContextService } from './../../common/services/datacontext.service';

import { PalindromeModel } from './../../common/models/palindrome.model';

@Injectable()
export class PalindromeService {

    constructor(private dataContext: DataContextService) {
    }

    getPalindromes() {
        return this.dataContext.httpGet('api/PalindromeData/GetPalindrome');
    }

    savePalindrome(palindrome: PalindromeModel) {
        return this.dataContext.httpPost('api/PalindromeData/SavePalindrome', palindrome);
    }
}
