describe("Test add collection page", () => {
  beforeEach(() => {
    cy.visit("/login")
      .get(".login form input")
      .first()
      .type("Martin")
      .get(".login form input")
      .eq(1)
      .type("martin11")
      .get(".login form input")
      .eq(2)
      .click();
  });

  it("should return error message", () => {
    cy.visit("/collection/add")
      .get("form input")
      .first()
      .type("а")
      .get("form input")
      .eq(1)
      .click()
      .get(".swal2-container *")
      .should(($elementsArray) => {
        expect($elementsArray.length).to.be.greaterThan(0);
      });
  });

  it("should add collection successfully", () => {
    cy.visit("/collection/add")
      .get("form input")
      .first()
      .type("test")
      .get("form input")
      .eq(1)
      .click();
    cy.location("pathname").should("eq", "/profile");
  });
});
