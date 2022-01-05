describe("Tests main functionalities", () => {
  it("loads posts", () => {
    cy.visit("/")
      .get(".posts a")
      .should(($photosArray) => {
        expect($photosArray.length).to.be.greaterThan(0);
      });
  });

  it("loads users", () => {
    cy.visit("/users")
      .get(".users a")
      .should(($usersArray) => {
        expect($usersArray.length).to.be.greaterThan(0);
      });
  });

  it("riderects to register page", () => {
    cy.visit("/login")
      .get(".login a")
      .first()
      .click();
    cy.location("pathname").should("eq", "/register");
  });
});
