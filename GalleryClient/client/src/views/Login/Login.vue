<template>
  <div class="login">
    <h1>Login</h1>
    <p class="error">{{ state.error }}</p>
    <form method="post" @submit.prevent="handleSubmit">
      <input
        type="email"
        v-model="state.email"
        name="email"
        placeholder="Email"
        required
      />
      <input
        type="text"
        v-model="state.username"
        name="username"
        placeholder="Username"
        required
      />
      <input
        type="password"
        v-model="state.password"
        name="password"
        placeholder="Password"
        required
      />
      <input type="submit" value="Send" class="submit-btn" />
    </form>
    <router-link to="/register">Don't have an account</router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive } from "vue";
import store from "../../store";
import { userService } from "../../services";

export default defineComponent({
  name: "Login",
  setup() {
    const state = reactive({
      email: "",
      username: "",
      password: "",
      error: "",
    });

    async function handleSubmit() {
      const { email, username, password } = state;

      const status = await userService.login(
        store.state.auth.state.token,
        email,
        username,
        password
      );

      if (status === 200) {
        store.state.auth.dispatch("login");
        console.log(store.state.auth.state);
        // window.location.href = "/";
      } else {
        state.error = "Something went wrong!";
        state.password = "";
      }
    }

    return {
      state,
      handleSubmit,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
