// export default defineOAuthKeycloakEventHandler({
//   async onSuccess(event, { user }) { ... }
// })


export default defineEventHandler(async (event) => {
    await setUserSession(event, {
        user: {
            name: "clown",
        },
        loggedInAt: new Date(),
      });
});