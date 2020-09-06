<template>
  <div class="login">
    <h1>Login</h1>
    <p class="error">{{error}}</p>
    <form method="post" @submit.prevent="handleSubmit">
      <input type="email" v-model="email" name="email" placeholder="Email" required />
      <input type="text" v-model="username" name="username" placeholder="Username" required />
      <input type="password" v-model="password" name="password" placeholder="Password" required />
      <input type="submit" value="Send" class="submit-btn" />
    </form>
    <router-link to="/register">Don't have an account</router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import router from "../router";
import { userService } from "../services";

export default defineComponent({
  name: "Login",
  data() {
    return {
      email: "",
      username: "",
      password: "",
      error: "",
    };
  },
  methods: {
    async handleSubmit() {
      const { email, username, password } = this;

      const status = await userService.login(email, username, password);

      if (status === 200) {
        router.push("/");
      } else {
        this.error = "Something went wrong!";
        this.password = "";
      }
    },
  },
});
</script>

<style lang="scss">
.login {
  * {
    display: block;
    margin: 1em auto;
  }

  .error {
    color: red;
  }

  input {
    width: 30em;
    height: 2em;
  }

  .submit-btn {
    border: none;
    width: 30em;
    background: #111111;
    color: #ffff;
  }

  a {
    text-decoration: none;
    color: black;
  }

  a:hover {
    color: rgba(0, 0, 0, 0.467);
  }
}
</style>