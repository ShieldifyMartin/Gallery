<template>
  <div class="login">
    <h1>Login</h1>
    <form method="post">
        <input type="text" name="email" placeholder="Email">
        <input type="text" name="username" placeholder="Username">
        <input type="password" name="password" placeholder="Password">
        <input type="submit" value="Send" class="submit-btn">
    </form>
    <router-link to="/register">Don't have an account</router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'Login' 
});
</script>

<style lang="scss">
    .login {
        * {
            display: block;
            margin: 1em auto;
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