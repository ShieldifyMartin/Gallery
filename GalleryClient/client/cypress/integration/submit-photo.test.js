describe("Test submit photo", () => {
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

  it("should return invalid data", () => {
    cy.visit("/submit")
      .fixture("testPicture.png")
      .then((fileContent) => {
        cy.get(".submit-photo form input")
          .first()
          .attachFile({
            fileContent: fileContent.toString(),
            fileName: "testPicture.png",
            mimeType: "image/png",
          });
      })
      .get(".submit-photo form input")
      .eq(1)
      .type("location text")
      .get(".submit-photo form input")
      .eq(2)
      .type("description text")
      .get(".submit-photo form input")
      .eq(3)
      .click()
      .get(".swal2-container *")
      .should(($elementsArray) => {
        expect($elementsArray.length).to.be.greaterThan(0);
      });
  });
});
