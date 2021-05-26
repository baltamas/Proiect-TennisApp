"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var matches_service_1 = require("./matches.service");
describe('MatchesService', function () {
    beforeEach(function () { return testing_1.TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = testing_1.TestBed.get(matches_service_1.MatchesService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=matches.service.spec.js.map