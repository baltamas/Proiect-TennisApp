"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var players_service_1 = require("./players.service");
describe('PlayersService', function () {
    beforeEach(function () { return testing_1.TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = testing_1.TestBed.get(players_service_1.PlayersService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=players.service.spec.js.map